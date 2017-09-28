
import unittest
import os

from invest import model, factory, database

class AllModuleTestCase(unittest.TestCase):
    """
        일단 모든 모듈의 테스트를 한 파일에서 진행
    """

    def test_document_factory(self):

        schema = factory.DocumentFactory.create_document(os.path.abspath('./Resource/helloworld/HelloWorld.xsd'))
        self.assertIsInstance(schema, model._SchemaDocument)
        instance = factory.DocumentFactory.create_document(os.path.abspath('./Resource/helloworld/HelloWorld.xml'))
        self.assertIsInstance(instance, model._InstanceDocument)

    def test_database_connection(self):
        db_credential = database.DbCredential(
            host = "localhost",
            database = "invest",
            user = "postgres",
            password = "passwd"
        )

        db_manager = database.DbManager(db_credential)
        self.assertIsInstance(db_manager, database.DbManager)
        self.assertIsNotNone(db_manager._connection)

if __name__ == '__main__':
    unittest.main()