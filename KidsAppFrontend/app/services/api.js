import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';


const API_BASE_URL = 'http://localhost:5166'; 

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});


api.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);


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
    console.log("burada2");
    const response = await api.post('/api/auth/login', {
      email,
      password,
    });
    console.log(response);
    return response.data;
  } catch (error) {
    if (error.response) {
      console.log('Error Response:', error.response.data);
      throw new Error(error.response.data.message || 'Login failed.');
    } else if (error.request) {
      throw new Error('No response from server.');
    } else {
      throw new Error('An error occurred.');
    }
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

export default api;
