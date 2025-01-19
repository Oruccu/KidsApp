// src/services/api.js
import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Platform } from 'react-native';
import * as jwtDecode from 'jwt-decode';

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

// İstek interceptor'ı
api.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('token');
    console.log('Making request to:', `${config.baseURL}${config.url}`);

    if (token) {
      const cleanToken = token.trim();
      config.headers.Authorization = `Bearer ${cleanToken}`;

      try {
        const payload = jwtDecode(cleanToken);
        console.log('Token payload:', payload);

        if (!payload.sub) {
          console.error('Token does not contain sub claim');
          await AsyncStorage.removeItem('token');
          throw new Error('Invalid token');
        }
      } catch (error) {
        console.error('Token decode error:', error);
        await AsyncStorage.removeItem('token');
        throw new Error('Invalid token');
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

// Yanıt interceptor'ı
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
      },
    });
    return Promise.reject(error);
  }
);

// Auth API Fonksiyonları
export const registerChild = async (email, password, userName, parentUserName) => {
  try {
    const response = await api.post('/api/Auth/registerChild', {
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
        const payload = jwtDecode(token);
        
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

export const parentLogin = async (email, password) => {
  try {
    const response = await api.post('/api/Auth/parentLogin', { 
      email, 
      password 
    });

    const { isSucced, token, message, userName } = response.data;

    if (!isSucced) {
      throw new Error(message || 'Parent login failed.');
    }

    if (token) {
      try {
        const payload = jwtDecode(token);

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

    await AsyncStorage.setItem('userName', userName);
    console.log('UserName kaydedildi:', userName);

    return response.data;
  } catch (error) {
    console.error('Parent login hatası:', error);
    throw error;
  }
};

export const logout = async () => {
  try {
    const response = await api.post('/api/Auth/logout');
    console.log('Logout successful:', response.data);

    await AsyncStorage.removeItem('token');
    await AsyncStorage.removeItem('childId');
    await AsyncStorage.removeItem('userName');
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
    const token = await AsyncStorage.getItem('token');
    if (!token) {
      throw new Error('No token found.');
    }

    const response = await api.delete('/api/Auth/deleteAccount', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
    console.log('Delete success:', response.data);

    await AsyncStorage.removeItem('token');
    await AsyncStorage.removeItem('childId');
    await AsyncStorage.removeItem('userName');
    return response.data;
  } catch (error) {
    console.error('Delete error:', error);
    throw error;
  }
};

export const getCurrentUser = async () => {
  try {
    const response = await api.get('/api/Auth/getCurrentUser');
    return response.data;
  } catch (error) {
    console.error('Get Current User error:', error);
    throw error;
  }
};

// Diğer API Fonksiyonları (KidsMode, User, AudioBook, AudioAnimal)
export const createKidsMode = async (email, password, userName, parentUserName) => {
  try {
    const response = await api.post('/api/KidsMode/create', {
      email,
      password,
      userName,
      parentUserName,
    });
    return response.data;
  } catch (error) {
    console.error('Create KidsMode error:', error);
    throw error;
  }
};

export const addKidsMode = async (childId, kidsModeDto) => {
  try {
    const response = await api.post(`/api/KidsMode/${childId}/kidsMode`, kidsModeDto);
    return response.data;
  } catch (error) {
    console.error('Add KidsMode error:', error);
    throw error;
  }
};

export const updateKidsMode = async (kidsModeDto) => {
  try {
    const response = await api.put('/api/KidsMode/update', kidsModeDto);
    return response.data;
  } catch (error) {
    console.error('Update KidsMode error:', error);
    throw error;
  }
};

export const getKidsMode = async (childId) => {
  try {
    const response = await api.get(`/api/KidsMode/${childId}`);
    return response.data;
  } catch (error) {
    console.error('Get KidsMode error:', error);
    throw error;
  }
};

export const deleteKidsMode = async (childId) => {
  try {
    const response = await api.delete(`/api/KidsMode/${childId}/kidsMode`);
    return response.data;
  } catch (error) {
    console.error('Delete KidsMode error:', error);
    throw error;
  }
};

export const getAllUsers = async () => {
  try {
    const response = await api.get('/api/User');
    return response.data;
  } catch (error) {
    console.error('Get All Users error:', error);
    throw error;
  }
};

export const getUserById = async (id) => {
  try {
    const response = await api.get(`/api/User/${id}`);
    return response.data;
  } catch (error) {
    console.error('Get User By ID error:', error);
    throw error;
  }
};

export const updateUser = async (id, updateUserDto) => {
  try {
    const response = await api.put(`/api/User/${id}`, updateUserDto);
    return response.data;
  } catch (error) {
    console.error('Update User error:', error);
    throw error;
  }
};

export const patchUser = async (id, patchDoc) => {
  try {
    const response = await api.patch(`/api/User/${id}`, patchDoc);
    return response.data;
  } catch (error) {
    console.error('Patch User error:', error);
    throw error;
  }
};

export const deleteUserById = async (id) => {
  try {
    const response = await api.delete(`/api/User/${id}`);
    return response.data;
  } catch (error) {
    console.error('Delete User error:', error);
    throw error;
  }
};

export const setScore = async (id, scoreDto) => {
  try {
    const response = await api.post(`/api/User/${id}/score`, scoreDto);
    return response.data;
  } catch (error) {
    console.error('Set Score error:', error);
    throw error;
  }
};

export const addFavoriteBook = async (childId, audioBookId) => {
  try {
    const response = await api.post(`/api/User/${childId}/favorite-book/${audioBookId}`);
    return response.data;
  } catch (error) {
    console.error('Add Favorite Book error:', error);
    throw error;
  }
};

export const getFavorites = async (childId) => {
  try {
    const response = await api.get(`/api/User/${childId}/favorite-books`);
    return response.data;
  } catch (error) {
    console.error('Get Favorites error:', error);
    throw error;
  }
};

export const getAudioBookByIdService = async (id) => {
  try {
    const response = await api.get(`/api/AudioBook/${id}`);
    return response.data;
  } catch (error) {
    console.error('Get AudioBook By ID error:', error);
    throw error;
  }
};

export const addAnimalSound = async (createAnimalSoundDto) => {
  try {
    const response = await api.post('/api/AudioAnimal', createAnimalSoundDto);
    return response.data;
  } catch (error) {
    console.error('Add Animal Sound error:', error);
    throw error;
  }
};

export default api;
