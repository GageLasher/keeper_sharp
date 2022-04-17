import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class KeepsService {
    async getAll() {
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
    }
    setActive(keep) {
        AppState.activeKeep = keep
        logger.log(AppState.activeKeep)

    }
    async removeVaultKeep(id) {
        const res = await api.delete('api/vaultkeeps/' + id)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }
    async removeKeep(id, keepId) {
        const res = await api.delete('api/keeps/' + keepId)
    }
    async create(body) {
        const res = await api.post('api/keeps/', body)
        AppState.keeps = [...AppState.keeps, res.data]
        AppState.activeProfileKeeps = [...AppState.activeProfileKeeps, res.data]
    }

}


export const keepsService = new KeepsService()