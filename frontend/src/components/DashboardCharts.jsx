import { Line, Bar, Radar } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  BarElement,
  RadialLinearScale,
  Tooltip,
  Legend
} from 'chart.js';

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, BarElement, RadialLinearScale, Tooltip, Legend);

export function DashboardCharts() {
  const lineData = {
    labels: ['W1', 'W2', 'W3', 'W4', 'W5', 'W6'],
    datasets: [{ label: 'Growth Trajectory', data: [92, 98, 110, 121, 130, 145], borderColor: '#7c5cff' }]
  };

  const radarData = {
    labels: ['Impulse', 'Consistency', 'Finance', 'Stress', 'Recovery'],
    datasets: [{ label: 'Personality Stability', data: [70, 80, 74, 65, 82], borderColor: '#31d0aa' }]
  };

  const barData = {
    labels: ['Low', 'Medium', 'High', 'Critical'],
    datasets: [{ label: 'Financial Exposure', data: [6, 12, 8, 3], backgroundColor: '#ff5470' }]
  };

  return (
    <section className="grid grid-2">
      <div className="card"><Line data={lineData} /></div>
      <div className="card"><Radar data={radarData} /></div>
      <div className="card"><Bar data={barData} /></div>
      <div className="card pulse">Simulation probability distribution & confidence intervals render from API payload.</div>
    </section>
  );
}
