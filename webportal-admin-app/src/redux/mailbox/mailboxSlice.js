import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./mailboxAsyncThunk";

export const initMailBox = {
  fromemail: "",
  toemail: "",
  cc: "",
  bcc: "",
  subject: "",
  body: "",
  languageid: "vi-VN",
  websiteid: 0,
};

const initialState = {
  loading: false,
  list: [],
  item: initMailBox,
};

export const mailboxSlice = createSlice({
  name: "mailbox",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initMailBox;
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
            (a, b) => new Date(b.datecreated) - new Date(a.datecreated)
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
            (a, b) => new Date(b.datecreated) - new Date(a.datecreated)
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

export const { select, unselect } = mailboxSlice.actions;

export const mailboxSelector = (state) => state.mailbox;

export default mailboxSlice.reducer;
