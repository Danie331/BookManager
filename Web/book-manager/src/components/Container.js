import { React, useState } from "react";
import BookList from "./BookList";
import CreateModal from "./CreateModal";

const Container = () => {
  const [show, setShow] = useState(false);
  return (
    <div className=" h-screen w-screen flex justify-center items-center flex-col">
      <div className=" w-full py-2 px-8 flex items-center justify-between">
        <h1 className=" text-blue-600 font-bold">Books</h1>
        <div className=" z-40">
          <button
            className=" text-white bg-blue-500 border border-blue-600 rounded-md outline-none px-3 py-1 hover:text-blue-600 hover:bg-white"
            onClick={() => setShow(!show)}
          >
            {show? "close" : "Add new record"}
          </button>
        </div>
      </div>
      <BookList />
      {show ? (
        <div className=" absolute h-full w-full backdrop-blur-md flex justify-center items-center z-10">
          <CreateModal />
        </div>
      ) : null}
    </div>
  );
};
export default Container;