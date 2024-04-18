import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./productInCategoryAsyncThunk";

export const initProductInCategory = {
  productid: 0,
  categoryid: 0,
};

const initialState = {
  loading: false,
  list: [],
  item: initProductInCategory,
};

export const productInCategorySlice = createSlice({
  name: "productInCategory",
  initialState,
  reducers: {
    clearAll: (state) => {
      state.list = [];
    },
  },
  extraReducers: (builder) => {
    builder

      .addCase(asyncThunk.getByProductIdAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getByProductIdAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload;
        }
      });
  },
});
export const { clearAll } = productInCategorySlice.actions;

export const productInCategorySelector = (state) => state.productInCategory;

export default productInCategorySlice.reducer;
