import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./bannerAsyncThunk";

export const initBanner = {
  image: "",
  status: 1,
  link: "",
  name: "",
  position: 0,
  incategories: "",
  ordernumber: 1,
  languageid: "vi-VN",
  detail: "",
  websiteid: 0,
};

const initialState = {
  loading: false,
  list: [],
  item: initBanner,
};

export const bannerSlice = createSlice({
  name: "banner",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initBanner;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(asyncThunk.getAllAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getAllAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.list = action.payload.sort((a, b) =>
            a.name > b.name ? 1 : a.name < b.name ? -1 : 0
          );
        }
        state.loading = false;
      })

      .addCase(asyncThunk.getPagingAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getPagingAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.list = action.payload.items.sort((a, b) =>
            a.name > b.name ? 1 : a.name < b.name ? -1 : 0
          );
        }
        state.loading = false;
      })

      .addCase(asyncThunk.getByIdAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getByIdAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.item = action.payload;
        }
        state.loading = false;
      })

      .addCase(asyncThunk.createAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.createAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.list = [action.payload].concat(state.list);
        }
        state.loading = false;
      })

      .addCase(asyncThunk.updateAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.updateAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.list = state.list.map((i) =>
            i.id === action.payload.id ? action.payload : i
          );
        }
        state.loading = false;
      })

      .addCase(asyncThunk.removeAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.removeAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.list = state.list.filter(
            (i) => i.id !== parseInt(action.payload)
          );
        }
        state.loading = false;
      });
  },
});

export const { select, unselect } = bannerSlice.actions;

export const bannerSelector = (state) => state.banner;

export default bannerSlice.reducer;
