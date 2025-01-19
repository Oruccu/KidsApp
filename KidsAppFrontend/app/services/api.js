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

export default api;
