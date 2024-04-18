import axios from "axios";
const apiUrl = "/appuserroles";

export const getByUserId = (id) => {
  return axios.get(`${apiUrl}/getByUserId?id=${id}`);
};

export const roleAssign = (data) => {
  return axios.post(`${apiUrl}/roleAssign`, data);
};
