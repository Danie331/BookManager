import { React, useState, useEffect } from "react";
import { HandleFetch } from "../Api";

const BookList = () => {
    const [list, setList] = useState([]);
    useEffect(() => {
      const getList = async () => {
        try {
          await HandleFetch().then((data) => {
            setList(data);
          })
        } catch (err) {
          console.log(err);
        }
      };
      getList();
    }, []);  

  return (
    <div className=" h-full w-full">
      <div className=" mt-14">
        <div className=" font-semibold text-2xl flex w-1/2 justify-between pl-3 mb-8">
          <p>Id</p>
          <p>Author</p>
          <p>Title</p>
          <p>Published Date</p>
          <p>ISBN</p>
        </div>
        {list &&
          list.map((item) => {
            return (
        <div className=" pl-8 w-full flex mb-4">
          <div className=" w-1/2 flex justify-between mr-28 pr-10">
            <h2>item.Id</h2>
            <h2>item.author</h2>
            <h2>item.title</h2>
            <h2>item.publishedDate</h2>
            <h2>item.isbn</h2>
          </div>
          <span>
            <button className=" bg-green-400 text-white px-3 py-1 rounded-sm mr-4">Edit record</button>
            <button className=" bg-red-500 text-white px-3 py-1 rounded-sm">Delete record</button>
          </span>
        </div>
            );
        })}
      </div>
    </div>
  );
};

export default BookList;