import axios from "axios";
const apiUrl = "/folders";

export const getAll = () => {
  return axios.get(apiUrl);
};

export const getSub = (name) => {
  return axios.get(`${apiUrl}/sub?name=${name}`);
};

export const create = (data) => {
  return axios.post(apiUrl, data);
};

export const update = (data) => {
  return axios.put(apiUrl, data);
};

export const remove = (name) => {
  return axios.delete(`${apiUrl}/remove?name=${name}`);
};
