import api from './api';


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
