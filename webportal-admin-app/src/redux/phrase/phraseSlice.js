import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./phraseAsyncThunk";

export const initPhrase = {
  key: "",
  value: "",
  languageid: "vi-VN",
  ispin: false,
  websiteid: 0,
};

const initialState = {
  loading: false,
  list: [],
  item: initPhrase,
};

export const phraseSlice = createSlice({
  name: "phrase",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initPhrase;
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
          state.list = action.payload
            .sort((a, b) => (a.key > b.key ? 1 : a.key < b.key ? -1 : 0))
            .sort((a, b) => (a.ispin === b.ispin ? 0 : a.ispin ? -1 : 1));
        }
      })

      .addCase(asyncThunk.getPagingAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getPagingAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.list = action.payload.items
            .sort((a, b) => (a.key > b.key ? 1 : a.key < b.key ? -1 : 0))
            .sort((a, b) => (a.ispin === b.ispin ? 0 : a.ispin ? -1 : 1));
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

export const { select, unselect } = phraseSlice.actions;

export const phraseSelector = (state) => state.phrase;

export default phraseSlice.reducer;
