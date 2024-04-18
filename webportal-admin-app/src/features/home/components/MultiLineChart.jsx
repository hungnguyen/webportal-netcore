import React from "react";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
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
  Legend
);

export default function MultiLineChart({ labels, datasets, title }) {
  const classes = useStyles();
  const options = {
    maintainAspectRatio: false,
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: title,
        font: {
          size: 20,
        },
      },
    },
    scales: {
      x: {
        ticks: {
          maxTicksLimit: 10,
        },
      },
      y: {
        ticks: {
          maxTicksLimit: 10,
        },
      },
    },
  };

  const data = {
    labels,
    datasets,
  };
  return (
    <Paper className={classes.tablePaper + " " + classes.saleChart}>
      <Line options={options} data={data} height={400} />
    </Paper>
  );
}
