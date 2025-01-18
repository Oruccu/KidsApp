import { configureStore } from '@reduxjs/toolkit';
import modeReducer from './modeSlice';
import userReducer from './userSlice';

const store = configureStore({
  reducer: {
    mode: modeReducer,
    user: userReducer,
  },
});


export default store;
