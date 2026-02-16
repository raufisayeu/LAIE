import { NavLink, Route, Routes } from 'react-router-dom';
import { LandingPage } from './pages/LandingPage';
import { AuthPage } from './pages/AuthPage';
import { DashboardPage } from './pages/DashboardPage';
import { DecisionManagementPage } from './pages/DecisionManagementPage';
import { HabitTrackingPage } from './pages/HabitTrackingPage';
import { AnalyticsIntelligenceCenterPage } from './pages/AnalyticsIntelligenceCenterPage';
import { AdminControlPanelPage } from './pages/AdminControlPanelPage';
import { UserProfilePage } from './pages/UserProfilePage';

const navItems = [
  ['/', 'Landing'],
  ['/auth', 'Auth'],
  ['/dashboard', 'Dashboard'],
  ['/decisions', 'Decision Mgmt'],
  ['/habits', 'Habit Tracking'],
  ['/analytics', 'AI Center'],
  ['/admin', 'Admin'],
  ['/profile', 'Profile']
];

export default function App() {
  return (
    <div className="app-shell">
      <aside className="sidebar">
        <h1>LAIE</h1>
        {navItems.map(([to, label]) => (
          <NavLink key={to} to={to} className="nav-item">{label}</NavLink>
        ))}
      </aside>
      <main className="content">
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/auth" element={<AuthPage />} />
          <Route path="/dashboard" element={<DashboardPage />} />
          <Route path="/decisions" element={<DecisionManagementPage />} />
          <Route path="/habits" element={<HabitTrackingPage />} />
          <Route path="/analytics" element={<AnalyticsIntelligenceCenterPage />} />
          <Route path="/admin" element={<AdminControlPanelPage />} />
          <Route path="/profile" element={<UserProfilePage />} />
        </Routes>
      </main>
    </div>
  );
}
