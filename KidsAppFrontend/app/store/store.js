import { configureStore } from '@reduxjs/toolkit';
import modeReducer from './modeSlice.js';
import userReducer from './userSlice.js';

const store = configureStore({
  reducer: {
    mode: modeReducer,
    user: userReducer,
  },
});

export default store;
