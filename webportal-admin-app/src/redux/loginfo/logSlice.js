import { createSlice } from "@reduxjs/toolkit";
import moment from "moment";

const initialState = {
  list: [],
};

export const logSlice = createSlice({
  name: "log",
  initialState,
  reducers: {
    addLog: (state, action) => {
      state.list = state.list.concat({
        statusCode: action.payload.status ?? action.payload.data.statusCode,
        message: action.payload.data.error ?? action.payload.data.message,
        detail: action.payload.data.detail,
        id: moment().format("MMDDYYYYhmmss"),
      });
    },
  },
});

export const { addLog } = logSlice.actions;

export const logSelector = (state) => state.log;

export default logSlice.reducer;
