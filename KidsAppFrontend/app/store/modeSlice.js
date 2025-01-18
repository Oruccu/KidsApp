import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import {
  createKidsMode,
  updateKidsMode,
  getKidsMode,
  getChildIdFromToken,
} from '@/app/services/modeApi.js';


export const setMode = createAsyncThunk(
  'mode/setMode',
  async ({ modeType }, { rejectWithValue }) => {
    try {
      console.log(`setMode thunk başlatıldı: ${modeType}`);

      const childId = await getChildIdFromToken();
      if (!childId) {
        throw new Error('childId bulunamadı.');
      }

      const kidsModeDto = {
        ChildId: childId,
        Mode: modeType.charAt(0).toUpperCase() + modeType.slice(1).toLowerCase(),
      };

      console.log('kidsModeDto:', kidsModeDto);

      const existingMode = await getKidsMode(childId);
      if (existingMode) {
        console.log('Mode güncelleniyor:', kidsModeDto);
        const response = await updateKidsMode(kidsModeDto);
        console.log('Mode güncellendi:', response);
        return response;
      } else {
        console.log('Mode oluşturuluyor:', kidsModeDto);
        const response = await createKidsMode(kidsModeDto);
        console.log('Mode oluşturuldu:', response);
        return response;
      }
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
      
      const childId = await getChildIdFromToken();
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
    currentMode: 'Boy', 
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
