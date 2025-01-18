import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  userName: '',
  token: '',
  isLoggedIn: false,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser(state, action) {
      state.userName = action.payload.userName;
      state.token = action.payload.token;
      state.isLoggedIn = true;
    },
    clearUser(state) {
      state.userName = '';
      state.token = '';
      state.isLoggedIn = false;
    },
  },
});

export const { setUser, clearUser } = userSlice.actions;
export default userSlice.reducer;
