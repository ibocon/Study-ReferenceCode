"""
    Database와 상호작용하는데 필요한 Module
"""

from typing import NamedTuple
import psycopg2

"""
    데이터베이스와의 연결을 위해 필요한 데이터 구조
"""
DbCredential = NamedTuple('DbCredential',
                           [('host', str),
                            ('database', str),
                            ('user', str), 
                            ('password', str)])

class DbManager:
    """
        데이터베이스와의 연결 및 상호작용을 정의한 클래스
    """
    def __init__(self, credential):
        assert isinstance(credential, DbCredential)
        self._connection = psycopg2.connect(host = credential.host, 
                                            database = credential.database,
                                            user = credential.user,
                                            password = credential.password)
        
        cur = self._connection.cursor()
        print('PostgreSQL database version:')
        cur.execute('SELECT version()')
        # display the PostgreSQL database server version
        db_version = cur.fetchone()
        print(db_version)