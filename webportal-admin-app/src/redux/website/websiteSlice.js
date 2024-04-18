import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./websiteAsyncThunk";

export const initWebsite = {
  name: "",
  domain: "",
  folder: "",
  mobilefolder: "",
  domainalias: "",
  fromemail: "",
  smtpserver: "",
  smtpserverport: 25,
  smtpusername: "",
  smtpuserpassword: "",
  smtpssl: false,
  currency: "",
  uploadfolder: "",
  deliveryfee: 0,
  totalpageview: 0,
  projectname: "",
  projectlink: "",
  isdown: false,
  pagedown: "",
  isresetcache: false,
};

const initialState = {
  loading: false,
  list: [],
  item: initWebsite,
};

export const websiteSlice = createSlice({
  name: "website",
  initialState,
  reducers: {
    select: (state, action) => {
      state.item = state.list.find((i) => i.id === parseInt(action.payload));
    },
    unselect: (state) => {
      state.item = initWebsite;
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

export const { select, unselect } = websiteSlice.actions;

export const websiteSelector = (state) => state.website;

export default websiteSlice.reducer;
