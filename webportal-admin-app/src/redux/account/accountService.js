import axios from "axios";
const apiUrl = "/account";

export const login = (data) => {
  return axios.post(`${apiUrl}/login`, data);
};

export const logout = () => {
  return axios.post(`${apiUrl}/logout`);
};

export const getProfile = () => {
  return axios.get(`${apiUrl}/profile`);
};

export const checkLogin = () => {
  return axios.get(`${apiUrl}/checkLogin`);
};
