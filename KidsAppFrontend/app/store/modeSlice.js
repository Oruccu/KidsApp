import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { getKidsMode, createKidsMode, updateKidsMode } from '@/app/services/modeApi';
import { getChildId } from '@/app/services/api';

export const setMode = createAsyncThunk(
  'mode/setMode',
  async ({ modeType }, { rejectWithValue }) => {
    try {
      console.log(`setMode thunk başlatıldı: ${modeType}`);
      const childId = await getChildId();
      console.log('setMode childId:', childId);
      if (!childId) {
        throw new Error('childId bulunamadı.');
      }

      // İlk olarak mevcut kaydı kontrol ediyoruz
      let currentModeData = null;
      try {
        currentModeData = await getKidsMode(childId);
        console.log('Mevcut mode verisi bulundu:', currentModeData);
      } catch (error) {
        console.log('Mode verisi bulunamadı, create işlemi uygulanacak.');
      }

      const kidsModeDto = { mode: modeType, childId };

      if (currentModeData) {
        console.log('Update işlemi başlatılıyor.');
        await updateKidsMode(kidsModeDto);
      } else {
        console.log('Create işlemi başlatılıyor.');
        await createKidsMode(kidsModeDto);
      }

      const updatedData = await getKidsMode(childId);
      console.log('Güncel mode verisi:', updatedData);
      return updatedData;
    } catch (error) {
      console.error('setMode hatası:', error);
      return rejectWithValue(error.response?.data?.message || 'Mode ayarlanamadı.');
    }
  }
);

export const fetchMode = createAsyncThunk(
  'mode/fetchMode',
  async (_, { rejectWithValue }) => {
    try {
      console.log('fetchMode thunk başlatıldı');
      const childId = await getChildId();
      console.log('fetchMode childId:', childId);
      if (!childId) {
        throw new Error('childId bulunamadı.');
      }
      const response = await getKidsMode(childId);
      console.log('fetchMode başarılı:', response);
      return response;
    } catch (error) {
      console.error('fetchMode hatası:', error);
      return rejectWithValue(error.response?.data?.message || 'Mode alınamadı.');
    }
  }
);

const modeSlice = createSlice({
  name: 'mode',
  initialState: {
    currentMode: 'Girl',
    status: 'idle',
    error: null,
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(setMode.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(setMode.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.currentMode = action.payload.mode || action.payload.Mode;
      })
      .addCase(setMode.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.payload;
      })
      .addCase(fetchMode.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchMode.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.currentMode = action.payload.mode || action.payload.Mode;
      })
      .addCase(fetchMode.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.payload;
      });
  },
});

export default modeSlice.reducer;
