import api from './api';

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
