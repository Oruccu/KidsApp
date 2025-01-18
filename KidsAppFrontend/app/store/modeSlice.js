import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  currentMode: 'Gril',
};

const modeSlice = createSlice({
  name: 'mode',
  initialState,
  reducers: {
    setMode(state, action) {
      state.currentMode = action.payload; 
    },
  },
});

export const { setMode } = modeSlice.actions;
export default modeSlice.reducer;
