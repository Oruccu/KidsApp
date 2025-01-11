import axios from 'axios';

const API_BASE_URL = 'http://localhost:5166'; 
const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const registerParent = async (email, password) => {
  try {
    const response = await api.post(`${API_BASE_URL}/api/auth/registerParent`, {
      email,
      password,
    });
    return response.data;
  } catch (error) {
    if (error.response) {
      throw new Error(error.response.data.message || 'Parent registration failed.');
    } else if (error.request) {
      throw new Error('No response from server.');
    } else {
      throw new Error('An error occurred.');
    }
  }
};

export const registerChild = async (email, password, userName, parentUserId) => {
  try {
    const response = await api.post('/api/auth/registerChild', {
      email,
      password,
      userName,
      parentUserId,
    });
    return response.data;
  } catch (error) {
    if (error.response) {
      throw new Error(error.response.data.message || 'Child registration failed.');
    } else if (error.request) {
      throw new Error('No response from server.');
    } else {
      throw new Error('An error occurred.');
    }
  }
};

export const getParentUsers = async () => {
  try {
    const response = await api.get('/api/auth/parents');
    return response.data;
  } catch (error) {
    if (error.response) {
      throw new Error(error.response.data.message || 'Failed to fetch parents.');
    } else if (error.request) {
      throw new Error('No response from server.');
    } else {
      throw new Error('An error occurred.');
    }
  }
};
