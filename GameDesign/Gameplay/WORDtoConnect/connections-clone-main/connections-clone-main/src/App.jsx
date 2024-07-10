import { useEffect } from "react";
import "./App.css";
import Tile from "./components/tile/Tile";

function App() {
	const categories = [
		["thing 1", "thing 2", "thing 3", "thing 4"],
		["thing 11", "thing 12", "thing 13", "thing 14"],
		["thing 21", "thing 22", "thing 23", "thing 24"],
		["thing 31", "thing 32", "thing 33", "thing 34"],
	];

	const randomCategories = [
		["", "", "", ""],
		["", "", "", ""],
		["", "", "", ""],
		["", "", "", ""],
	];
	// useEffect(() => {
	// 	const randomizeCategories = () => {
	// 		for (let i = 0; i < categories.length; i++) {
	// 			for (let j = 0; j < categories[i].length; j++) {
	// 				let hasBeenFilled = false;
	// 				while (hasBeenFilled === false) {
	// 					const randomIIndex = Math.floor(Math.random() * 4);
	// 					const randomJIndex = Math.floor(Math.random() * 4);
	// 					if (randomCategories[randomIIndex][randomJIndex] === "") {
	// 						randomCategories[randomIIndex][randomJIndex] = categories[i][j];
	// 						hasBeenFilled = true;
	// 					}
	// 				}
	// 			}
	// 		}
	// 	};

	// 	randomizeCategories();
	// 	console.log(randomCategories);
	// }, [categories, randomCategories]);

	return (
		<div className="App">
			<div className="container">
				{randomCategories.map((category, i) => {
					category.map((word, j) => {
						<Tile word={word} key={word} />;
					});
				})}
			</div>
		</div>
	);
}

export default App;
