import api from './api';

export const getAudioBookById = async (id) => {
  try {
    const response = await api.get(`/api/AudioBook/${id}`);
    return response.data;
  } catch (error) {
    console.error('Get AudioBook By ID error:', error);
    throw error;
  }
};
