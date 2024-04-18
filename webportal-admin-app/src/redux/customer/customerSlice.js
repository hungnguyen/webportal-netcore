import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./customerAsyncThunk";

export const initCustomer = {
  username: "",
  email: "",
  fullname: "",
  idcard: "",
  address: "",
  country: "",
  phonenumber: "",
  status: 1,
  city: "",
  district: "",
  birthday: "",
  gender: 1,
  image: "",
  websiteid: 0,
};

const initialState = {
  loading: false,
  list: [],
  item: initCustomer,
  total: 0,
};

export const customerSlice = createSlice({
  name: "customer",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initCustomer;
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
            a.fullname > b.fullname ? 1 : a.fullname < b.fullname ? -1 : 0
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
            a.fullname > b.fullname ? 1 : a.fullname < b.fullname ? -1 : 0
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
          state.total = action.payload;
        }
      });
  },
});

export const { select, unselect } = customerSlice.actions;

export const customerSelector = (state) => state.customer;

export default customerSlice.reducer;
