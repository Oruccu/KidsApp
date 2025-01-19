import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Platform } from 'react-native';
import jwtDecode from 'jwt-decode';

const API_BASE_URL = Platform.select({
  ios: 'http://127.0.0.1:5166',
  android: 'http://10.0.2.2:5166',
});

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
});

api.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('token');
    console.log('Making request to:', `${config.baseURL}${config.url}`);
    
    if (token) {
      const cleanToken = token.trim();
      config.headers.Authorization = `Bearer ${cleanToken}`;
      
      try {
        const base64Url = cleanToken.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        const payload = JSON.parse(jsonPayload);
        console.log('Token payload:', payload);
        
        if (!payload.sub) {
          console.error('Token does not contain sub claim');
          await AsyncStorage.removeItem('token');
          throw new Error('Invalid token');
        }
      } catch (error) {
        console.error('Token decode error:', error);
        throw error;
      }
    } else {
      console.warn('No token found for request');
    }
    
    return config;
  },
  (error) => {
    console.error('Request interceptor error:', error);
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => {
    console.log('Response status:', response.status);
    console.log('Response data:', response.data);
    return response;
  },
  (error) => {
    console.error('Response error:', {
      message: error.message,
      status: error.response?.status,
      data: error.response?.data,
      config: {
        url: error.config?.url,
        method: error.config?.method,
      }
    });
    return Promise.reject(error);
  }
);



export const getChildId = async () => {
  const childIdStr = await AsyncStorage.getItem('childId');
  if (!childIdStr) return null;
  return parseInt(childIdStr, 10);
};

export const registerChild = async (email, password, userName, parentUserName) => {
  try {
    const response = await api.post('/api/auth/registerChild', {
      email,
      password,
      userName,
      parentUserName,
    });

    return response.data;
  } catch (error) {

    if (error.response) {
      console.log('Error Response:', error.response.data);
      throw new Error(error.response.data.message || 'Child registration failed.');
    } else if (error.request) {

      console.log('No response from server:', error.request);
      throw new Error('No response from server.');
    } else {

      throw new Error('An error occurred.');
    }
  }
};

export const login = async (email, password) => {
  try {
    const response = await api.post('/api/Auth/login', { 
      email, 
      password 
    });
    
    const { isSucced, token, message, childId } = response.data;
    
    if (!isSucced) {
      throw new Error(message || 'Giriş başarısız.');
    }

    if (token) {
      try {
        // jwtDecode yerine decodeToken kullanılıyor
        const payload = decodeToken(token);
        
        if (!payload || !payload.sub) {
          throw new Error('Geçersiz token formatı');
        }
        
        await AsyncStorage.setItem('token', token);
        console.log('Token başarıyla kaydedildi');
      } catch (error) {
        console.error('Token doğrulama hatası:', error);
        throw new Error('Sunucudan geçersiz token alındı');
      }
    }

    if (childId) {
      await AsyncStorage.setItem('childId', String(childId));
      console.log('ChildId kaydedildi:', childId);
    }

    return response.data;
  } catch (error) {
    console.error('Giriş hatası:', error);
    throw error;
  }
};


export const getAudioBookById = async (id) => {
  try {
    const response = await api.get(`/api/AudioBook/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching audiobook:', error);
    throw error;
  }
};

export const logout = async () => {
  try {
    const response = await api.post('/api/auth/logout');
    console.log('Logout successful:', response.data);

    await AsyncStorage.removeItem('token');
    return response.data;
  } catch (error) {
    if (error.response) {
      console.log('Error Response:', error.response.data);
      throw new Error(error.response.data.message || 'Logout failed.');
    } else if (error.request) {
      throw new Error('No response from server.');
    } else {
      throw new Error('An error occurred.');
    }
  }
};
export const deleteLoggedInUser = async () => {
  try {
    console.log('delete kısmında geldi');
    const response = await axios.delete('/api/auth/deleteAccount', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
    console.log('Delete success:', response.data);
  } catch (error) {
    console.error('Delete error:', error);
  }
};


export default api;
