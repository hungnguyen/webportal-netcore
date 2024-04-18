import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./fileAsyncThunk";

const initialState = {
  loading: false,
  list: [],
  openFile: {},
  selectFile: {},
  isRefresh: false
};

export const fileSlice = createSlice({
  name: "file",
  initialState,
  reducers: {
    select: (state, action) => {
      state.selectFile = action.payload;
    },
    open: (state, action) => {
      state.openFile = action.payload;
    },
    setIsRefresh: (state, action) => {
      state.isRefresh = action.payload;
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
          state.isRefresh = false;
        }
      })

      .addCase(asyncThunk.getSubAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getSubAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload.sort((a, b) =>
            a.name > b.name ? 1 : a.name < b.name ? -1 : 0
          );
          state.isRefresh = false;
        }
      })

      .addCase(asyncThunk.createAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.createAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = [action.payload].concat(state.list);
          state.isRefresh = true;
        }
      })
      
      .addCase(asyncThunk.updateAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.updateAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = state.list.map((i) =>
            i.name === action.payload.name ? action.payload : i
          );
          state.isRefresh = true;
        }
      })

      .addCase(asyncThunk.removeAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.removeAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = state.list.filter(
            (i) => i.name !== action.payload
          );
          state.isRefresh = true;
        }
      });
  },
});

export const { select, open, setIsRefresh } = fileSlice.actions;

export const fileSelector = (state) => state.file;

export default fileSlice.reducer;
