import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  currentTo: "",
  messages: [
    // {from:"",to:"",message:""}
  ],
};

export const chatSlice = createSlice({
  name: "chat",
  initialState,
  reducers: {
    selectTo: (state, action) => {
      state.currentTo = action.payload;
    },
    addMessage: (state, action) => {
      state.messages = state.messages.concat(action.payload);
    },
    updateTo: (state, action) => {
      if (state.currentTo === "") {
        state.currentTo = action.payload;
      }
    },
  },
});

export const { selectTo, addMessage, updateTo } = chatSlice.actions;

export const chatSelector = (state) => state.chat;

export default chatSlice.reducer;
