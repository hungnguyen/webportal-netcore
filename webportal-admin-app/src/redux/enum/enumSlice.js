import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./enumAsyncThunk";

const initialState = {
  status: [],
  gender: [],
  position: [],
  paymethod: [],
  paystatus: [],
  orderstatus: [],
};

export const enumSlice = createSlice({
  name: "enums",
  initialState,
  extraReducers: (builder) => {
    builder
      .addCase(asyncThunk.getGenderAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.gender = action.payload;
        }
      })
      .addCase(asyncThunk.getStatusAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.status = action.payload;
        }
      })
      .addCase(asyncThunk.getPayMethodAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.paymethod = action.payload;
        }
      })
      .addCase(asyncThunk.getPayStatusAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.paystatus = action.payload;
        }
      })
      .addCase(asyncThunk.getPositionAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.position = action.payload;
        }
      })
      .addCase(asyncThunk.getOrderStatusAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.orderstatus = action.payload;
        }
      });
  },
});

export const enumSelector = (state) => state.enums;

export default enumSlice.reducer;
