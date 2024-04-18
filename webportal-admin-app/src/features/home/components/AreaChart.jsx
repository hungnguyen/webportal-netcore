import React from "react";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Filler,
  Legend,
} from "chart.js";
import { Line } from "react-chartjs-2";
import useStyles from "../../shared/styles";
import { Paper } from "@material-ui/core";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Filler,
  Legend
);

export default function AreaChart({ chartTitle, dataTitle, rows, labels }) {
  const classes = useStyles();
  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: chartTitle,
      },
    },
  };
  const data = {
    labels,
    datasets: [
      {
        fill: true,
        label: dataTitle,
        data: rows,
        borderColor: "rgb(53, 162, 235)",
        backgroundColor: "rgba(53, 162, 235, 0.5)",
      },
    ],
  };
  return (
    <Paper className={classes.tablePaper}>
      <Line options={options} data={data} />
    </Paper>
  );
}
