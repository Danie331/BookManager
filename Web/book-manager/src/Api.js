import React from "react";
import axios from "axios";

export const HandleFetch = async () => {
  const url = `http://localhost:5195/api/books`;
  return axios.get(url).then((res) => {
    return res.data.items;
  });
};