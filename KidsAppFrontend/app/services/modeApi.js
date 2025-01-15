import axios from 'axios';
import jwtDecode from 'jwt-decode';
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


export const getChildIdFromToken = async () => {
  const token = await AsyncStorage.getItem('token');
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    return decoded.nameid ? parseInt(decoded.nameid) : null; 
  } catch (error) {
    console.error('Token decode hatası:', error);
    return null;
  }
};

// Mode oluşturma
export const createKidsMode = async (kidsModeDto) => {
  try {
    console.log('API çağrısı: createKidsMode', kidsModeDto);
    const response = await api.post('/api/KidsMode/create', kidsModeDto);
    console.log('API cevabı: createKidsMode', response.data);
    return response.data;
  } catch (error) {
    console.error('createKidsMode API hatası:', error);
    throw error;
  }
};

// Mode güncelleme
export const updateKidsMode = async (kidsModeDto) => {
  try {
    console.log('API çağrısı: updateKidsMode', kidsModeDto);
    const response = await api.put('/api/KidsMode/update', kidsModeDto);
    console.log('API cevabı: updateKidsMode', response.data);
    return response.data;
  } catch (error) {
    console.error('updateKidsMode API hatası:', error);
    throw error;
  }
};

// Mode getirme
export const getKidsMode = async () => {
  try {
    const childId = await getChildIdFromToken();
    if (!childId) throw new Error('childId bulunamadı.');
    console.log(`API çağrısı: getKidsMode için ChildId=${childId}`);
    const response = await api.get(`/api/KidsMode`);
    console.log('API cevabı: getKidsMode', response.data);
    return response.data;
  } catch (error) {
    console.error('getKidsMode API hatası:', error);
    throw error;
  }
};
