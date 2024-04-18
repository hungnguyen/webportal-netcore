import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./categoryAsyncThunk";

export const initCategory = {
  name: "",
  parentid: 0,
  typecode: "",
  ispopular: false,
  image: "",
  displaytype: "",
  description: "",
  metatitle: "",
  metakey: "",
  metadescription: "",
  urlname: "",
  link: "",
  icon: "",
  shortdescription: "",
  status: 1,
  isontop: false,
  isonright: false,
  isonbottom: false,
  isonleft: false,
  isoncenter: false,
  ordernumber: 1,
  languageid: "vi-VN",
  websiteid: 0,
};

const initialState = {
  loading: false,
  list: [],
  item: initCategory,
};

export const categorySlice = createSlice({
  name: "category",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initCategory;
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
            (a, b) => a.ordernumber - b.ordernumber
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
            (a, b) => a.ordernumber - b.ordernumber
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
          state.list = [action.payload]
            .concat(state.list)
            .sort((a, b) => a.ordernumber - b.ordernumber);
        }
      })

      .addCase(asyncThunk.updateAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.updateAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = state.list
            .map((i) => (i.id === action.payload.id ? action.payload : i))
            .sort((a, b) => a.ordernumber - b.ordernumber);
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

export const { select, unselect } = categorySlice.actions;

export const categorySelector = (state) => state.category;

export default categorySlice.reducer;
