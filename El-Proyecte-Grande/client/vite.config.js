import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  // https://stackoverflow.com/a/77234158/433835
  const env = loadEnv(mode, process.cwd())
  console.log(env.VITE_PROXY);

  return {
    plugins: [react()],
    server: {
    proxy: {
      "/api": {
        target: env.VITE_PROXY ? env.VITE_PROXY : 'http://localhost:5229/',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
      }
    }
    }
  }
})


