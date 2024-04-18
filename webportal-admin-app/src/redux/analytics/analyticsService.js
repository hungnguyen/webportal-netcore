import axios from "axios";
const apiUrl = "/analytics";

export const getSummary = (data) => {
  return axios.post(`${apiUrl}/summary`, data);
};

export const getGraph = (data) => {
  return axios.post(`${apiUrl}/graph`, data);
};

export const getTopList = (data) => {
  return axios.post(`${apiUrl}/toplist`, data);
};
