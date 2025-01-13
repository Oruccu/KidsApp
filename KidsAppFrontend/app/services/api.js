import axios from 'axios';

const API_BASE_URL = 'http://localhost:5166';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const registerChild = async (email, password, userName, parentUserName) => {
  try {
    const response = await api.post(`/api/auth/registerChild`, {
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
      console.log(error.request)
      throw new Error('No response from server.');
    } else {
      throw new Error('An error occurred.');
    }
  }
};


