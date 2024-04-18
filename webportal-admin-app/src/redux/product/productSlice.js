import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./productAsyncThunk";

export const initProduct = {
  languageid: "vi-VN",
  websiteid: 0,
  name: "",
  status: 1,
  ordernumber: 1,
  image: "",
  urlname: "",
  typecode: "PRD",
  ishot: false,
  isfeature: false,
  replateproduct: "",
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
  item: initProduct,
  total: 0,
  totalProduct: 0,
  totalNews: 0,
};

export const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initProduct;
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
          state.list = action.payload.sort(
            (a, b) => new Date(b.dateupdated) - new Date(a.dateupdated)
          );
        }
      })

      .addCase(asyncThunk.getPagingAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getPagingAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload.items.sort(
            (a, b) => new Date(b.dateupdated) - new Date(a.dateupdated)
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
      })

      .addCase(asyncThunk.getCountAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getCountAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          switch (action.payload.typecode) {
            case "PRD":
              state.totalProduct = action.payload.total;
              break;
            case "NWS":
              state.totalNews = action.payload.total;
              break;
            default:
              state.total = action.payload.total;
          }
        }
      });
  },
});

export const { select, unselect } = productSlice.actions;

export const productSelector = (state) => state.product;

export default productSlice.reducer;
