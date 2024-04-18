import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  list: [],
};

export const noticeSlice = createSlice({
  name: "notice",
  initialState,
  reducers: {
    showNotice: (state, action) => {
      state.list = state.list.concat({id: state.list.length + 1, ...action.payload});
    },
    hideAllNotice: (state) => {
      state.list = [];
    },
    hideNotice: (state, action) => {
      state.list = state.list.filter((i) => i.id !== action.payload);
    },
  },
});

export const { showNotice, hideNotice } = noticeSlice.actions;

export const noticeSelector = (state) => state.notice;

export default noticeSlice.reducer;
