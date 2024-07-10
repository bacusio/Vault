import React from "react";
import "./Tile.css";
import { useState } from "react";

const Tile = (props) => {
	const [selected, setSelected] = useState(false);
	const tileClass = selected ? "tile selected" : "tile normal";

	const handleClick = () => {
		setSelected(!selected);
	};

	return (
		<div className={tileClass} onClick={handleClick}>
			<div className="word">{props.word}</div>
		</div>
	);
};

export default Tile;
