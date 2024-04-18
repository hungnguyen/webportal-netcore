import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./accountAsyncThunk";
import * as appUserAsyncThunk from "../appuser/appuserAsyncThunk";

const emptyProfile = {
  username: "",
  fullname: "",
  image: "",
};

const initialState = {
  login: false,
  loading: false,
  checking: false,
  profile: emptyProfile,
  error: "",
};

export const accountSlice = createSlice({
  name: "account",
  initialState,
  reducers: {
    logout: (state, action) => {
      state.login = false;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(asyncThunk.loginAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.loginAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          if (action.payload.status === 400) {
            state.error = action.payload.error;
          } else {
            state.login = true;
          }
        }
      })

      .addCase(asyncThunk.logoutAsync.fulfilled, (state) => {
        state.login = false;
        state.profile = emptyProfile;
      })

      .addCase(asyncThunk.getProfileAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.profile = action.payload;
        }
      })

      .addCase(asyncThunk.checkLoginAsync.pending, (state) => {
        state.checking = true;
      })
      .addCase(asyncThunk.checkLoginAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.login = true;
        }
        state.checking = false;
      })

      .addCase(appUserAsyncThunk.updateAsync.fulfilled, (state, action) => {
        if (action.payload) {
          if (action.payload.id === state.profile.id) {
            state.profile = action.payload;
          }
        }
      });
  },
});

export const { logout } = accountSlice.actions;

export const accountSelector = (state) => state.account;

export default accountSlice.reducer;
