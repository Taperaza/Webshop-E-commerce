import React, {useState, useEffect} from "react";
import {Link} from 'react-router-dom';

function Categories(){
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        fetch('https://localhost:44388/api/categories/', {method: 'GET', mode:'cors'})
            .then(res => res.json())
            .then(
                (result) => {
                    setCategories(result);
                }
            )
    })

    return (
        <div style={{textAlign: "center"}}> CATEGORIES <br />
            {categories.map((category) => 
                <a href={`/products/${category.CategoryId}`}>
                    <button key={category.CategoryId} > {category.CategoryDescription} </button>
                </a>
            )}
        </div>
    )
}

export default Categories;