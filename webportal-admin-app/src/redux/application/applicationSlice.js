import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "../website/websiteAsyncThunk";

const initialState = {
  loading: false,
  ischeck: false,
  imageBaseAddress: "http://localhost/wprimages/",
  languageid: "vi-VN",
  website: {
    id: 1,
  },
};

export const applicationSlice = createSlice({
  name: "application",
  initialState,
  reducers: {
    clearAll: (state) => {
      state.list = [];
    },
  },
  extraReducers: (builder) => {
    builder

      .addCase(asyncThunk.getByDomainAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getByDomainAsync.fulfilled, (state, action) => {
        state.loading = false;
        state.ischeck = true;
        state.website = action.payload;
      })

      .addCase(asyncThunk.updateAsync.fulfilled, (state, action) => {
        state.loading = false;
        state.website = action.payload;
      });
  },
});
export const { clearAll } = applicationSlice.actions;

export const applicationSelector = (state) => state.application;

export default applicationSlice.reducer;
