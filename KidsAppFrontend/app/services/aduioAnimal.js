import api from './api';

export const addAnimalSound = async (createAnimalSoundDto) => {
  try {
    const response = await api.post('/api/AudioAnimal', createAnimalSoundDto);
    return response.data;
  } catch (error) {
    console.error('Add Animal Sound error:', error);
    throw error;
  }
};
