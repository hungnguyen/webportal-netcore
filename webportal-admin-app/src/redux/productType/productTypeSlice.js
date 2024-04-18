import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./productTypeAsyncThunk";

export const initProductType = {
  name: "",
  code: "",
  languageid: "vi-VN",
  ispublic: false,
  status: 1,
  websiteid: 0,
  text1: "",
  text2: "",
  text3: "",
  text4: "",
  text5: "",
  text6: "",
  text7: "",
  text8: "",
  text9: "",
  text10: "",
  text11: "",
  text12: "",
  text13: "",
  text14: "",
  text15: "",
  text16: "",
  text17: "",
  text18: "",
  text19: "",
  text20: "",
  desc1: "",
  desc2: "",
  desc3: "",
  desc4: "",
  desc5: "",
  desc6: "",
  desc7: "",
  desc8: "",
  desc9: "",
  desc10: "",
};

const initialState = {
  loading: false,
  list: [],
  item: initProductType,
};

export const productTypeSlice = createSlice({
  name: "productType",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initProductType;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(asyncThunk.getAllAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getAllAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload.sort((a, b) =>
            a.name > b.name ? 1 : a.name < b.name ? -1 : 0
          );
        }
      })

      .addCase(asyncThunk.getPagingAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getPagingAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload.items.sort((a, b) =>
            a.name > b.name ? 1 : a.name < b.name ? -1 : 0
          );
        }
      })

      .addCase(asyncThunk.getByIdAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getByIdAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.item = action.payload;
        }
      })

      .addCase(asyncThunk.createAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.createAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = [action.payload].concat(state.list);
        }
      })

      .addCase(asyncThunk.updateAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.updateAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = state.list.map((i) =>
            i.id === action.payload.id ? action.payload : i
          );
        }
      })

      .addCase(asyncThunk.removeAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.removeAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = state.list.filter(
            (i) => i.id !== parseInt(action.payload)
          );
        }
      });
  },
});

export const { select, unselect } = productTypeSlice.actions;

export const productTypeSelector = (state) => state.productType;

export default productTypeSlice.reducer;
