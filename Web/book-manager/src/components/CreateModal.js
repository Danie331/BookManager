import {React, useState, useEffect} from "react";
const CreateModal = () => {
//   const [Id, setId] = useState("");
  const [Author, setAuthor] = useState("");
  const [Title, setTitle] = useState("");
  const [PublishedDate, setPublishedDate] = useState("");
  const [Isbn, setIsbn] = useState("");
  return (
    <div className=" h-1/2 w-3/4 bg-slate-100 shadow-lg pt-4 flex items-center flex-col">
      <p className=" text-center text-blue-500 text-2xl font-bold mb-10">
        Create new record
      </p>
      <div className="flex justify-center items-center w-4/5 flex-col gap-8">
        <input
          className="w-full border-2 border-blue-500 p-2"
          type="text"
          placeholder="Author"
          value={Author}
          onChange={(e) => setAuthor(e.target.value)}
        />
        <input
          className="w-full border-2 border-blue-500 p-2"
          type="text"
          placeholder="Title"
          value={Title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <input
          className="w-full border-2 border-blue-500 p-2"
          type="date"
          placeholder="PublishedDate"
          value={PublishedDate}
          onChange={(e) => setPublishedDate(e.target.value)}
        />
        <input
          className="w-full border-2 border-blue-500 p-2"
          type="text"
          placeholder="Isbn"
          value={Isbn}
          onChange={(e) => setIsbn(e.target.value)}
        />
      </div>
      <div className=" w-4/5 flex justify-end mt-8">
        <button className=" bg-blue-500 text-white px-3 py-1 rounded-sm">
          Create Record
        </button>
      </div>
    </div>
  );
};
export default CreateModal;