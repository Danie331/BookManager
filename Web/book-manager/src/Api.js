import React from "react";
import axios from "axios";

export const HandleFetch = async () => {
  const url = `https://localhost:7014/api/Books`;
  return axios.get(url).then((res) => {
    return res.data.items;
  });
};