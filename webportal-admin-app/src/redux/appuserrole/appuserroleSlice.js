import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./appuserroleAsyncThunk";

const initialState = {
  loading: false,
  list: [],
};

export const appuserroleSlice = createSlice({
  name: "appuserrole",
  initialState,
  reducers: {
    clearAll: (state) => {
      state.list = [];
    },
  },
  extraReducers: (builder) => {
    builder

      .addCase(asyncThunk.getByUserIdAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getByUserIdAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload;
        }
      });
  },
});
export const { clearAll } = appuserroleSlice.actions;

export const appuserroleSelector = (state) => state.appuserrole;

export default appuserroleSlice.reducer;
