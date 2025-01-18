import api from './api';

export const createKidsMode = async (kidsModeDto) => {
  console.log('API çağrısı: createKidsMode', kidsModeDto);
  const response = await api.post('/api/KidsMode/create', kidsModeDto);
  return response.data;
};

export const updateKidsMode = async (kidsModeDto) => {
  console.log('API çağrısı: updateKidsMode', kidsModeDto);
  const response = await api.put('/api/KidsMode/update', kidsModeDto);
  return response.data;
};

export const getKidsMode = async (childIdParam) => {
  let childId = childIdParam;
  if (!childId) {
    // AsyncStorage’den childId alabilirsiniz.
    // Eğer childId yoksa hata fırlatıyoruz.
    throw new Error('childId bulunamadı.');
  }
  const response = await api.get(`/api/KidsMode/${childId}`);
  return response.data;
};
