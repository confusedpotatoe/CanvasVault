import { useEffect, useState, type FormEvent } from 'react'
import axios from 'axios'
import './App.css'

type AuthSession = {
  username: string
  token: string
}

const STORAGE_KEY = 'canvasvault.auth'

function App() {
  const [session, setSession] = useState<AuthSession | null>(null)
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [rememberMe, setRememberMe] = useState(true)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [errorMessage, setErrorMessage] = useState('')

  useEffect(() => {
    const savedSession = window.localStorage.getItem(STORAGE_KEY)

    if (!savedSession) {
      return
    }

    try {
      setSession(JSON.parse(savedSession) as AuthSession)
    } catch {
      window.localStorage.removeItem(STORAGE_KEY)
    }
  }, [])

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setErrorMessage('')
    setIsSubmitting(true)

    try {
      const response = await axios.post<AuthSession>('http://localhost:5132/api/auth/login', {
        username,
        password,
      })

      setSession(response.data)

      if (rememberMe) {
        window.localStorage.setItem(STORAGE_KEY, JSON.stringify(response.data))
      } else {
        window.localStorage.removeItem(STORAGE_KEY)
      }
    } catch {
      setErrorMessage('Could not sign in. Check your username and password.')
    } finally {
      setIsSubmitting(false)
    }
  }

  function handleSignOut() {
    setSession(null)
    window.localStorage.removeItem(STORAGE_KEY)
    setUsername('')
    setPassword('')
    setRememberMe(true)
    setErrorMessage('')
  }

  if (session) {
    return (
      <main className="dashboard-shell">
        <header className="dashboard-topbar">
          <div>
            <p className="eyebrow">CanvasVault</p>
            <h1>Welcome back, {session.username}</h1>
          </div>
          <button type="button" className="secondary-button" onClick={handleSignOut}>
            Sign out
          </button>
        </header>

        <section className="dashboard-copy" aria-label="Dashboard overview">
          <p className="eyebrow">Index page</p>
          <h2>Your vault is ready</h2>
          <p>
            Use this home screen to jump into artworks, collections, and future review
            tools. Your session is active, your token is stored, and the app is ready
            for the next step.
          </p>
          <p>
            Quick stats: 12 saved artworks, 4 collections, and 9 recent updates.
            Browse the vault, review your work, or sign out when you are done.
          </p>
          <p className="token-text">Current session token:</p>
          <code>{session.token}</code>
          <div className="copy-actions">
            <a href="/">Browse artworks</a>
            <a href="/">View collections</a>
          </div>
        </section>
      </main>
    )
  }

  return (
    <main className="page-shell">
      <section className="hero-panel" aria-labelledby="hero-title">
        <div className="brand-pill">CanvasVault</div>
        <h1 id="hero-title">Collect, organize, and review your art in one place.</h1>
        <p className="hero-copy">
          A clean workspace for artworks and collections, built for a fast login
          flow and a focused first impression.
        </p>

        <p className="hero-sub">
          Fast access to your saved artworks, neat collections to group related
          pieces, and a focused review surface so you can see the essentials
          without clutter.
        </p>
      </section>

      <section className="login-panel" aria-labelledby="login-title">
        <div className="login-copy">
          <p className="eyebrow">Welcome back</p>
          <h2 id="login-title">Sign in to CanvasVault</h2>
          <p>Use your account to continue to the dashboard.</p>

          <form className="login-form" onSubmit={handleSubmit}>
            <label>
              <span>Username</span>
              <input
                type="text"
                name="username"
                placeholder="admin"
                value={username}
                onChange={(event) => setUsername(event.target.value)}
                autoComplete="username"
                required
              />
            </label>

            <label>
              <span>Password</span>
              <input
                type="password"
                name="password"
                placeholder="••••••••"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                autoComplete="current-password"
                required
              />
            </label>

            <div className="login-form__meta">
              <label className="remember-me">
                <input
                  type="checkbox"
                  name="remember"
                  checked={rememberMe}
                  onChange={(event) => setRememberMe(event.target.checked)}
                />
                <span>Remember me</span>
              </label>
              <a href="/">Forgot password?</a>
            </div>

            {errorMessage ? <p className="form-error">{errorMessage}</p> : null}

            <button type="submit" className="primary-button" disabled={isSubmitting}>
              {isSubmitting ? 'Signing in...' : 'Sign in'}
            </button>
          </form>

          <p className="login-footer">
            New to the app? <a href="/">Create an account</a>
          </p>
        </div>
      </section>
    </main>
  )
}

export default App
