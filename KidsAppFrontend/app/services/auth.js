// src/services/auth.js
import api from './api';
import AsyncStorage from '@react-native-async-storage/async-storage';
import jwtDecode from 'jwt-decode';

// Register Child
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

// Login
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

// Parent Login
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

// Logout
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

// Delete Logged-In User
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

// Get Current User
export const getCurrentUser = async () => {
  try {
    const response = await api.get('/api/Auth/getCurrentUser');
    return response.data;
  } catch (error) {
    console.error('Get Current User error:', error);
    throw error;
  }
};
