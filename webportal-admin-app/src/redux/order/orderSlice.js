import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./orderAsyncThunk";

export const initOrder = {
  orderstatus: 0,
  promotioncode: "",
  discount: 0,
  totalamount: 0,
  fee: 0,
  totalnetamount: 0,
  paymethod: 0,
  paystatus: 0,
  shippingname: "",
  shippingaddress: "",
  shippingphone: "",
  shippingemail: "",
  findus: "",
  languageid: "vi-VN",
  websiteid: 0,
  customer: {
    fullname: "",
    email: "",
    phonenumber: "",
    address: "",
  },
  orderitems: [],
};

const initialState = {
  loading: false,
  list: [],
  item: initOrder,
  total: 0,
};

export const orderSlice = createSlice({
  name: "order",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initOrder;
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
            (a, b) => new Date(b.orderdate) - new Date(a.orderdate)
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
            (a, b) => new Date(b.orderdate) - new Date(a.orderdate)
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

      .addCase(asyncThunk.getDetailByIdAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getDetailByIdAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          state.item = action.payload;
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

export const { select, unselect } = orderSlice.actions;

export const orderSelector = (state) => state.order;

export default orderSlice.reducer;
