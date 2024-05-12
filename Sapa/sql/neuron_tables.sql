Create Table Neurons(
	neuron_id INT PRIMARY KEY
);

Create Table MotorNeurons(
	motor_id INT PRIMARY KEY,
	neuron_id INT FOREIGN KEY references Neurons
);

Create Table SensorNeurons(
	sensor_id INT PRIMARY KEY,
	neuron_id INT FOREIGN KEY references Neurons
);

Create Table InterNeurons(
	inter_id INT PRIMARY KEY,
	neuron_id INT FOREIGN KEY references Neurons
);