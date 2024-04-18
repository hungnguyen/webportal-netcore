import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./approleAsyncThunk";

export const initAppRole = {
  description: "",
  name: "",
  normalizedname: "",
};

const initialState = {
  loading: false,
  list: [],
  item: initAppRole,
};

export const approleSlice = createSlice({
  name: "approle",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === action.payload);
    },
    unselect: (state) => {
      state.item = initAppRole;
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
          state.list = state.list.filter((i) => i.id !== action.payload);
        }
      });
  },
});

export const { select, unselect } = approleSlice.actions;

export const approleSelector = (state) => state.approle;

export default approleSlice.reducer;
